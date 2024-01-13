import { Injectable } from '@angular/core';
import {AngularFireMessaging} from "@angular/fire/compat/messaging";
import {environment} from "../../environments/environment";
import {messaging} from "../../configs/firebase.config";

@Injectable({
  providedIn: 'root'
})
export class PushNotificationsService {

  constructor(private angularFireMessaging: AngularFireMessaging) {
    this.angularFireMessaging.messages.subscribe(
      (msg) => {
        console.log("Push Notification Received", msg);
      });
  }

  requestPermission() {
    messaging.getToken({vapidKey: environment.firebaseConfig.vapidKey}).then(
      (token) => {
        console.log(token);
      },
      (err) => {
        console.error('Unable to get permission to notify.', err);
      }
    );
  }

  listen() {
    messaging.onMessage((payload) => {
      console.log('Message received. ', payload);
    });
  }

  areNotificationsAllowed() {
    return Notification.permission === 'granted';
  }
}
