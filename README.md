# vNumbers
Make own massive gateway for 1000+ world SMS virtual number

  * Incoming SMS crawler
  * Outgoing SMS driver
  * User-defined actions

## Example of database

Based on LiteDB 5.0.11

```
/* 708 */
{
  "_id": {"$oid": "612fd2632589c944f055ff24"},
  "Carrier": "Unknown",
  "Country": "Netherlands",
  "Domain": "quackr.io",
  "Sender": "MICO",
  "Receiver": "+3197010238021",
  "Text": "475668 is your PIN code",
  "Hash": "A356838F3E0C28950560FC7917DA3734E4DAE47B",
  "ReceivedDateTtime": {"$date": "2021-09-01T02:20:03.7410000Z"},
  "ConfirmedDateTime": {"$date": "2021-09-01T19:20:03.7410000Z"}
}
/* 709 */
{
  "_id": {"$oid": "612fd2632589c944f055ff25"},
  "Carrier": "Unknown",
  "Country": "Netherlands",
  "Domain": "quackr.io",
  "Sender": "Microsoft",
  "Receiver": "+3197010238021",
  "Text": "509326 Use this code for Microsoft verification.",
  "Hash": "9E4DE3524646EEECF2E203302303335F8FF9898B",
  "ReceivedDateTtime": {"$date": "2021-08-31T23:20:03.7410000Z"},
  "ConfirmedDateTime": {"$date": "2021-09-01T19:20:03.7410000Z"}
}
/* 710 */
{
  "_id": {"$oid": "612fd26c2589c944f055ff26"},
  "Carrier": "Unknown",
  "Country": "United States",
  "Domain": "quackr.io",
  "Sender": "*******8936",
  "Receiver": "+12013797889",
  "Text": "Account: 358307 is your Samsung account verification code.",
  "Hash": "6C9F205143170471B95FF51CC341641441717980",
  "ReceivedDateTtime": {"$date": "2021-09-01T18:45:12.0840000Z"},
  "ConfirmedDateTime": {"$date": "2021-09-01T19:20:12.0840000Z"}
}
```

## Example of debugging

```
[Meete -> +447418362256] 907937
[Meete -> +447418362256] 922443
[Ankama -> +447418362256] Le code confidentiel de validation de votre numero de telephone est 886078  (Pseudo foi*********). Bon jeu !
[Lyft -> +447418362256] Lyft account activated.  Get free credit for inviting friends.
[Uber -> +447418362256] Your Uber code: 5853. Reply STOP to +44 7903 561836 to unsubscribe.
[DISCORD -> +447418362256] Your Discord security code is: 135 750
[NOTICE -> +447418362256] Paycam Wallet Code [170884]
[Verify -> +447418362256] Your one-time eBay PIN is 5904
[InfoSMS -> +447418362256] [Netease]Your pin code is 425944.--Netease CloudGaming
[Amazon -> +447418362256] 358364 is your Amazon OTP. Do not share it with anyone.
[MailRu -> +447418362256] TamTam: 9724 - number confirmation code
[Verify -> +447418362256] Your one-time eBay PIN is 1456
[JiuJia -> +447418362256] [Netease]Your pin code is 253619.--Netease CloudGaming
[MICO -> +447418362256] 544182 is your PIN code
[MICO -> +447418362256] 859477 is your PIN code
[Apple -> +447418362256] Your Apple ID code is: 512683. Do not share it with anyone.
[Cash App -> +447418362256] Cash App: 07418 362256 has been unlinked from your Cash App account
[DISCORD -> +447418362256] Your Discord security code is: 073 581
[Evernote -> +447418362256] Your Evernote Verification code is: 742827
[WorldRemit -> +447418362256] Thank you for updating your WorldRemit account details.
[SKRILL -> +447418362256] 812753 is your Skrill authentication code.
[Adobe -> +447418362256] 286962 is your Adobe code. Not you? Change your password.
[Meete -> +447418362256] 907937
[Meete -> +447418362256] 92244
```
