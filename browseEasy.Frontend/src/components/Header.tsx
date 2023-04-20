import { FC, useContext } from "react";
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
        if (user !== null) {
            users.map(u => {
                if (u.name !== user.displayName && user.displayName !== null) {
                    const newUser: Partial<IUser> = {
                        name: user.displayName
                    }
                    postUsers(newUser);
                }
            })
        }
    })
    openForm();
    }

    return (
        <>
       { users.map(user => user.name.split('r')[0])}
        <p>Home!</p>
        <button onClick={login}>Get started</button>
        </>
    )
}