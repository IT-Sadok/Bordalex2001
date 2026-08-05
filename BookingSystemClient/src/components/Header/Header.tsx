import { useState } from "react";
import { Link } from "react-router";

function SearchBar() {
  const [query, setQuery] = useState("");

  return (
    <>
      <form role="search" className="max-w-md mx-auto w-full h-full px-4">
        <div className="relative flex items-center w-full h-full">
          <input
            type="search"
            value={query}
            onChange={(e) => setQuery(e.target.value)}
            placeholder="Search..."
            className="w-full px-4 py-2.5 border border-gray-300 rounded-3xl focus:ring-2 focus:ring-blue-500 transition-all"
          />
          <div className="absolute right-0 flex items-center pr-2">
            <button
              type="button"
              className="bg-red-500 hover:bg-red-600 text-white font-semibold py-2 px-2 rounded-2xl transition-all"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 16 16"
                fill="currentColor"
                className="size-4"
              >
                <path
                  fillRule="evenodd"
                  d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                  clipRule="evenodd"
                />
              </svg>
            </button>
          </div>
        </div>
      </form>
    </>
  );
}

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
          <SearchBar />
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
