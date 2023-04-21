import { FC, useContext, useState } from "react";
import { auth, signInWithGoogle } from '../services/firebase';
import { UserContext } from "../App";
import { IActiveUser, IUser } from "../services/interfaces";
import { postUsers } from "../services/api";

type HeaderProps = {
    openForm: () => void
    activeUser: (user: IActiveUser) => void
}

export const Header: FC<HeaderProps> = ({openForm, activeUser}) => {
    const [user, setUser] = useState<string>();

    const login = () => {
    if (user === null) {
        signInWithGoogle();
    }
    auth.onAuthStateChanged(user => {
         if (user !== null && user.displayName !== null && user.uid !== "") {
            const newUser: Partial<IUser> = {
                name: user.displayName,
                loginId: user.uid
            }
            postUsers(newUser);
            const setActiveUser = {
                id: user.uid,
                name: user.displayName
            }
            activeUser(setActiveUser);
            setUser(setActiveUser.name);
        }
        openForm();
    })
    }

    return (
        <>
        <p>Home!</p>
        <button onClick={login}>Get started</button>
        </>
    )
}