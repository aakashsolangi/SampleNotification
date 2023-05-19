# SampleNotification

Postman collection of Sample has also been added

Get the Firebase Token

SampleNotification.Droid.Services => RegistrationIntentService => SendRegistrationToAppServer(Token)
Get the token from SendRegistrationToAppServer method and put into the Postman collection json in "to" property. You will be able to get the notification.

Issue 

If Notifications are working fine Put app i n background open any other app send the notfication from Postman. When you recieved notfication tapped o nnotfication it will trigger SampleNotification.Droid.Services => NotificationIntentService method calls "OnHandleIntent" In which I created a new intent for SPlash Actvity and pass this intent to StartActivity method. It did not return anything or invoke splas activity.
