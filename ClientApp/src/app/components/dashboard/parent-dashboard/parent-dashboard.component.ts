import {Component, OnInit} from '@angular/core';
import {PushNotificationsService} from "../../../services";
import {MatDialog} from "@angular/material/dialog";
import {
  NotificationsPermissionsModalComponent
} from "../../notification-permissions-modal/notifications-permissions-modal.component";

@Component({
  selector: 'app-parent-dashboard',
  templateUrl: './parent-dashboard.component.html',
  styleUrl: './parent-dashboard.component.css'
})

export class ParentDashboardComponent implements OnInit {
   role = localStorage.getItem('role');

   constructor(public dialog: MatDialog, private pushService: PushNotificationsService) {}

   ngOnInit(): void {
     this.openNotificationsModal();
   }

   openNotificationsModal(): void {
     if (localStorage.getItem('notificationsPermission') === 'true') {
        return;
      }
     localStorage.setItem('notificationsPermission', 'true');
    const dialogRef = this.dialog.open(NotificationsPermissionsModalComponent, {
      width: '700px'
    });
   }
}
