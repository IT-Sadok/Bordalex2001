import { Link, Outlet } from "react-router-dom";

export default function MainLayout() {
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
            <main>
                <Outlet />
            </main>
            <footer>
                <p>&copy; 2026 Booking System. All rights reserved.</p>
            </footer>
        </>
    )
}