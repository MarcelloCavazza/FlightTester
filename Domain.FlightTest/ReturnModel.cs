using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FlightTest
{
    public class ReturnModel
    {
        private const string errorMessage = "The Action failed, but no error message was already defined";
        private string Message { get; set; }
        private bool Sucess { get; set; }

        public bool GetSucess()
        {
            return this.Sucess;
        }
        public string GetMessage()
        {
            return this.Message;
        }
        public ReturnModel(bool sucess) {
            if (!sucess)
            {
                throw new Exception(errorMessage);
            }
            this.Sucess = sucess;
        }
        public ReturnModel(bool sucess, string message) {
            if(!sucess && String.IsNullOrEmpty(message))
            {
                throw new Exception(errorMessage);
            }
            this.Sucess = sucess;
            this.Message = message;
        }
    }
}
