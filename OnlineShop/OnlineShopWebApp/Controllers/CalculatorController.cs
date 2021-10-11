﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class CalculatorController : Controller
    {
        public string Index(double a, double b, string mathOperation)
        {

            if (!string.IsNullOrEmpty(mathOperation))
            {
                switch (mathOperation)
                {
                    case "+":
                        return $"{a} + {b} = {a + b}";
                    case "-":
                        return $"{a} - {b} = {a - b}";                  
                    case "*":
                        return $"{a} * {b} = {a * b}";
                    default:
                        return "Неверная операция!\nДоступные операции:\n+ сложение\n- вычитание\n* умножение";
                }
            }
            else return $"{a} + {b} = {a + b}"; 
            }
        }
    }

