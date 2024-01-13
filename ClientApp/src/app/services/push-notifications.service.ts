import { Injectable } from '@angular/core';
import {AngularFireMessaging} from "@angular/fire/compat/messaging";
import {environment} from "../../environments/environment";
import {messaging} from "../../configs/firebase.config";
import {
  NotificationsPermissionsModalComponent
} from "../components/notification-permissions-modal/notifications-permissions-modal.component";
import {MatDialog} from "@angular/material/dialog";
import {
  NotificationReceivedModalComponent
} from "../components/notification-received-modal/notification-received-modal.component";
import {UserService} from "./user.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class PushNotificationsService {
  currentUserId !: string;
  constructor(private angularFireMessaging: AngularFireMessaging, public dialog: MatDialog, public userService: UserService, private http: HttpClient) {
    this.angularFireMessaging.messages.subscribe(
      async (msg) => {
        console.log('Push notification received', msg);

        alert(msg.data?.title + ': ' + msg.data?.body);
      //
      //   const dialogRef = this.dialog.open(NotificationReceivedModalComponent, {
      //     width: '700px',
      //     data: {
      //       'title': msg.data?.title,
      //       'body': msg.data?.body,
      //     }
      //   });
      });
  }

  addTokenToCurrentUser(token: string) {
    const addTokenDto = {
      personId: this.currentUserId,
      token: token
    };

    this.http.post(environment.apiBaseUrl + '/Notification/token', addTokenDto).subscribe(
      (res) => {
        console.log(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  requestPermission() {
    Notification.requestPermission().then((permission) => {
      messaging.getToken({vapidKey: environment.firebaseConfig.vapidKey}).then(
        (token) => {
          this.addTokenToCurrentUser(token);
        }).catch((err) => {
          console.error(err);
        });
    }).catch(
      (err) => {
        console.error('Unable to get permission to notify.', err);
      });
  }

  listenForMessage() {
    messaging.onMessage((payload) => {
      console.log('Message received. ', payload);
      if (this.currentUserId === payload.data.userId) {
        this.openNotificationModal(payload.data);
      }
    });
  }

  openNotificationModal(data: any) {
    return new Promise( resolve => setTimeout(resolve, 100000)).then(() => {
      const dialogRef = this.dialog.open(NotificationReceivedModalComponent, {
        width: '700px',
        data: data
      });
    });
  }

  areNotificationsAllowed() {
    return Notification.permission === 'granted';
  }
}
