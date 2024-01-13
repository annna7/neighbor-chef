import { Component } from '@angular/core';
import {MatDialogRef} from "@angular/material/dialog";
import {PushNotificationsService} from "../../services";

@Component({
  selector: 'app-notifications-modal',
  templateUrl: './notifications-modal.component.html',
  styleUrl: './notifications-modal.component.css'
})
export class NotificationsModalComponent {
  constructor(public dialogRef: MatDialogRef<NotificationsModalComponent>,
              private pushService: PushNotificationsService) {}

  onAllow(): void {
    this.pushService.requestPermission();
    this.dialogRef.close();
  }

  onDeny(): void {
    this.dialogRef.close();
  }
}
