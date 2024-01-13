import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {BehaviorSubject} from "rxjs";

// TODO: WHY DOESN'T THIS WORK??
@Component({
  selector: 'app-notification-received-modal',
  templateUrl: './notification-received-modal.component.html',
  styleUrl: './notification-received-modal.component.css'
})
export class NotificationReceivedModalComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<NotificationReceivedModalComponent>,
              @Inject(MAT_DIALOG_DATA) public data: { title: string, body: string }) {}

  ngOnInit() {

  }

  onClose(): void {
    this.dialogRef.close();
  }
}
