Netopia Test Project
=======

This project helps you test Netopia Payments (also known as mobilpay) in .Net Core.


Install & Use
-------------

Follow these steps to create an account and generate signature and keys (also presented here: https://suport.mobilpay.ro/index.php?/Knowledgebase/Article/View/68/11/documentatie-fgo).
Please note it will not request you to pay anything for sandbox environment.
1. First you need to create an account at https://admin.netopia-payments.com/
2. Go to 'Dezvoltare' > 'Implementare' and use the wizard (choose Java when asks for you patform)
3. After you get to the 'Dezvoltare - Pornește implementarea' page, click on 'MEDIU DE TEST' to open the sandbox
4. Login with the same credentials as on live. You will always see a 'SANBOX' text on the top blue header when on sandbox.
5. Go to 'Puncte de vanzare' > 'Vezi lista punctelor de vanzare' and click on 'SETARI TEHNICE'
6. Copy the signature ('Semnatura') and overwrite in appsettings.json
7. Download the private and public keys, by clicking on 'DESCARCA' on 'Cheie privata' and 'Cheie publica' and fill the download location in appsettings.json
8. Run the project an click on 'Make Payment'


Test cards can be found here:
https://suport.mobilpay.ro/index.php?/Knowledgebase/Article/View/57/0/carduri-de-test

MobilPay will make a POST request to your confirm url.

TODO
----

The project is incomplete. To finish it we should add:

- the confirmation post method
- remove dummy data
- tests


Disclaimer
----------
This library is not associated in any way with Netopia Payments or mobilPay. You can use it freely at your will.