import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  isCollapsed = false;

  constructor() {
    if ('serviceWorker' in navigator) {
      navigator.serviceWorker.register('../../firebase-messaging-sw.js', { type: 'module' }).then((registration) => {
        console.log('Registration successful, scope is:', registration.scope);
      }).catch((err) => {
        console.log('Service worker registration failed, error:', err);
      });
    }
  }
}
