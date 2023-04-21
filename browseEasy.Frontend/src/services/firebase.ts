import firebase from 'firebase/compat/app';
import 'firebase/compat/auth';

const firebaseConfig = {
  apiKey: "AIzaSyAhSA7SfVIoG4XQYbnLQnn7YR5LVkWcASc",
  authDomain: "browse-easy-4bd45.firebaseapp.com",
  projectId: "browse-easy-4bd45",
  storageBucket: "browse-easy-4bd45.appspot.com",
  messagingSenderId: "564398196634",
  appId: "1:564398196634:web:82d66dda8c01d603cc9c23",
  measurementId: "G-CFNG5ML5Z5"
};

// Initialize Firebase 
firebase.initializeApp(firebaseConfig);
export const auth = firebase.auth();

const provider = new firebase.auth.GoogleAuthProvider();
provider.setCustomParameters({ prompt: 'select_account' });

export const signInWithGoogle = () => auth.signInWithPopup(provider);