using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomer
{
    bool CanPay { get; set; }
    void MakePayment();

    void PaymentStackAnimation(Transform transform);
}
