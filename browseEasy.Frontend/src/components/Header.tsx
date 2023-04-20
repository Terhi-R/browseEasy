import { FC } from "react";

type HeaderProps = {
    openForm: () => void
}

export const Header: FC<HeaderProps> = ({openForm}) => {
    const homePage = () => {
        openForm();
    }

    return (
        <>
        <p>Home!</p>
        <button onClick={homePage}>Click me!</button>
        </>
    )
}