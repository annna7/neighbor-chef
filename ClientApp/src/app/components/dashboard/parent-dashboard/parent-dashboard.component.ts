import {Component, OnInit} from '@angular/core';
import {PushNotificationsService} from "../../../services";
import {MatDialog} from "@angular/material/dialog";
import {NotificationsModalComponent} from "../../notifications-modal/notifications-modal.component";

@Component({
  selector: 'app-parent-dashboard',
  templateUrl: './parent-dashboard.component.html',
  styleUrl: './parent-dashboard.component.css'
})

export class ParentDashboardComponent implements OnInit {
   role = localStorage.getItem('role');

   constructor(public dialog: MatDialog, private pushService: PushNotificationsService) {}

   ngOnInit(): void {
     if (!this.pushService.areNotificationsAllowed()) {
       this.openNotificationsModal();
     }
   }

   enablePushNotifications(): void {
     this.pushService.requestPermission();
   }

   openNotificationsModal(): void {
    const dialogRef = this.dialog.open(NotificationsModalComponent, {
      width: '700px'
    });
   }
}
