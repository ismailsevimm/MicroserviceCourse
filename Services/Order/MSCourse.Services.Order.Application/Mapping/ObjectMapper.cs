﻿using AutoMapper;
using System;

namespace MSCourse.Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(()=>
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<GeneralMapping>(); } );

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
