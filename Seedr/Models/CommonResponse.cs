﻿namespace Seedr.Models
{
    public class CommonResponse : Error
    {
        public bool Result { get; set; }
        public int Code { get; set; }
    }
}
