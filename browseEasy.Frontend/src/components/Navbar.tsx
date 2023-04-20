import { Link } from "react-router-dom";

export const Navbar = () => {
    return (
        <>
        <div className="navbar">
        <Link to="/settings">Settings</Link>
        <Link to="/about">About</Link>
        </div>
        </>
    )
}