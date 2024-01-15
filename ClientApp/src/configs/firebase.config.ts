import firebase from 'firebase/compat/app';
import 'firebase/compat/messaging';
import {environment} from "../environments/environment";

export const app = firebase.initializeApp(environment.firebaseConfig);
export const messaging = firebase.messaging(app);
