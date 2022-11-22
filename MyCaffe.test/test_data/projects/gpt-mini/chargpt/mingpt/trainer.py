"""
Simple training loop; Boilerplate that could apply to any arbitrary neural network,
so nothing in this file really has anything to do with GPT specifically.
"""

import time
from collections import defaultdict

import torch
from torch.utils.data.dataloader import DataLoader
from mingpt.utils import CfgNode as CN

from typing import Iterator, Iterable, Optional, Sequence, List, TypeVar, Generic, Sized, Union

class Trainer:

    @staticmethod
    def get_default_config(model_type = None):
        C = CN()
        # device to train on
        C.device = 'auto'
        # dataloder parameters
        C.num_workers = 0 #_CHANGE_ 
        # optimizer parameters
        C.max_iters = None
        C.batch_size = 64
       
        if model_type == 'gpt-pico' or model_type == 'gpt-pico3':
            C.batch_size = 1

        if model_type == 'gpt-picoB' or model_type == 'gpt-pico3B':
            C.batch_size = 2        
            
        if model_type == 'gpt-pico3B5':
            C.batch_size = 5
            
        C.learning_rate = 3e-4
        C.betas = (0.9, 0.95)
        C.weight_decay = 0.1 # only applied on matmul weights
        C.grad_norm_clip = 0.0 # _CHANGE_ disable
        C.model_type = model_type
        return C
    
    def __init__(self, config, model, train_dataset):
        self.config = config
        self.model = model
        self.optimizer = None
        self.train_dataset = train_dataset
        self.callbacks = defaultdict(list)
        self.debug = False #_CHANGE_

        # determine the device we'll train on
        if config.device == 'auto':
            self.device = 'cuda' if torch.cuda.is_available() else 'cpu'
        else:
            self.device = config.device
        self.model = self.model.to(self.device)
        print("running on device", self.device)

        # variables that will be assigned to trainer class later for logging and etc
        self.iter_num = 0
        self.iter_time = 0.0
        self.iter_dt = 0.0

    def add_callback(self, onevent: str, callback):
        self.callbacks[onevent].append(callback)

    def set_callback(self, onevent: str, callback):
        self.callbacks[onevent] = [callback]

    def trigger_callbacks(self, onevent: str):
        for callback in self.callbacks.get(onevent, []):
            callback(self)

    def run(self):
        model, config = self.model, self.config
        
        # setup the optimizer
        self.optimizer = model.configure_optimizers(config)
        # setup the dataloader
        train_loader = DataLoader(
            self.train_dataset,
            #sampler=torch.utils.data.RandomSampler(self.train_dataset, replacement=True, num_samples=int(1e10)), # _CHANGE_
            sampler=torch.utils.data.SequentialSampler(self.train_dataset),
            shuffle=False,
            pin_memory=True,
            batch_size=config.batch_size,
            num_workers=config.num_workers,
        )
        
        model.train()
        self.iter_num = 0
        self.iter_time = time.time()
        data_iter = iter(train_loader)
        while True:
            # Set to True to generate 'iter_#' directories
            model.set_iter(self.iter_num, self.debug)
            # fetch the next batch (x, y) and re-init iterator if needed
            try:
                batch = next(data_iter)
            except StopIteration:
                data_iter = iter(train_loader)
                batch = next(data_iter)
            batch = [t.to(self.device) for t in batch]
            x, y = batch
            
            if self.debug:
                model.save_internal_blobs()

            # forward the model
            logits, self.loss = model(x, y)
            
            # backprop and update the parameters
            model.zero_grad(set_to_none=True)          
            self.loss.backward()
            
            if self.debug:
                model.save_internal_grad()
            
            if config.grad_norm_clip > 0:
                torch.nn.utils.clip_grad_norm_(model.parameters(), config.grad_norm_clip)
            self.optimizer.step()
            
            if self.debug:
                model.save_internal_grad("after_step")
            
            self.trigger_callbacks('on_batch_end')
            self.iter_num += 1
            tnow = time.time()
            self.iter_dt = tnow - self.iter_time
            self.iter_time = tnow

            # termination conditions
            if config.max_iters is not None and self.iter_num >= config.max_iters:
                break