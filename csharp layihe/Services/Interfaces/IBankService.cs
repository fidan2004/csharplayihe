﻿using layihe1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace layihe1.Services.Interfaces
{
    public interface IBankService<T> where T : BaseModel
    {
        public void Create(T emplye);
        public void Update(string data1 , string data2);
        public bool Delete(string name);
        public T Get(string filter);
        public void GetAll();
    }
}