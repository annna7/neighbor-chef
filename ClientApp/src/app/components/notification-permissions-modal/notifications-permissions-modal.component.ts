import { Component } from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {PushNotificationsService} from "../../services";

@Component({
  selector: 'app-notifications-permissions-modal',
  templateUrl: './notifications-permissions-modal.component.html',
  styleUrl: './notifications-permissions-modal.component.css'
})
export class NotificationsPermissionsModalComponent {
  constructor(public dialogRef: MatDialogRef<NotificationsPermissionsModalComponent>,
              private pushService: PushNotificationsService) {}

  onAllow(): void {
    this.pushService.requestPermission();
    this.dialogRef.close();
  }

  onDeny(): void {
    this.dialogRef.close();
  }
}
