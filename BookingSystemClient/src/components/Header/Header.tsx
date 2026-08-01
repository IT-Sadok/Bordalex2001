import { Link } from "react-router-dom";

export default function Header() {
    return (
        <>
            <header>
                <h1>Booking System</h1>
                <nav>
                    <Link to="/">Home</Link> |
                    <Link to="/login">Login</Link> |
                    <Link to="/register">Register</Link>
                </nav>
            </header>
        </>
    )
}