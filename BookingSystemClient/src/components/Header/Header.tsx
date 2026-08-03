import { Link } from "react-router-dom";

export default function Header() {

  return (
    <>
      <header>
        <nav
          aria-label="Main Navigation"
          className="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8"
          role="navigation"
        >
          <div className="flex lg:flex-1">
            <Link to="/" className="-m-1.5 p-1.5">
              Booking System
            </Link>
          </div>
          <div className="hidden lg:flex lg:flex-1 lg:justify-end gap-x-12">
            <Link
              to="/login"
              className="text-sm/6 font-semibold text-gray-900 hover:text-blue-600"
            >
              Login
            </Link>
            <Link
              to="/register"
              className="text-sm/6 font-semibold text-gray-900 hover:text-blue-600"
            >
              Register
            </Link>
          </div>
        </nav>
      </header>
    </>
  );
}
