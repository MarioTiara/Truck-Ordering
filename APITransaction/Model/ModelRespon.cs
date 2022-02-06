using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReplenishmentService.APITransaction.Model
{
    public class ModelRespon
    {
        private string AssertMessage = "success";
        private bool Assertsucess = true;
        public string message {
            get {return this.AssertMessage ;}
            set { this.AssertMessage=value;}
        }
        public bool success {
            get {return this.Assertsucess ;}
            set {this.Assertsucess=value;}
        }
    }
}
