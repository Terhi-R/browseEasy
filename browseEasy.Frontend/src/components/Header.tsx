import { FC, useContext, useState } from "react";
import { auth, signInWithGoogle } from '../services/firebase';
import { UserContext } from "../App";
import { IUser } from "../services/interfaces";
import { postUsers } from "../services/api";

type HeaderProps = {
    openForm: () => void
}

export const Header: FC<HeaderProps> = ({openForm}) => {
    const users = useContext(UserContext);

    const login = () => {
    signInWithGoogle();
    auth.onAuthStateChanged(user => {
         if (user !== null && user.displayName !== null && user.uid !== "") {
            const newUser: Partial<IUser> = {
                name: user.displayName,
                loginId: user.uid
            }
            postUsers(newUser);
        } 
    })
    openForm();
    }

    return (
        <>
       {users.map(user => user.loginId)}
        <p>Home!</p>
        <button onClick={login}>Get started</button>
        </>
    )
}