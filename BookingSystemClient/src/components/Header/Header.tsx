import { Link } from "react-router";
import SearchBar from "./SearchBar/SearchBar";
import LanguageRegionMenu from "./LanguageRegionMenu";
import UserMenu from "./UserMenu";

export default function Header() {
  return (
    <>
      <header className="border-b border-gray-200 bg-white">
        <nav
          aria-label="Main Navigation"
          className="mx-auto w-full px-4 sm:px-6 lg:px-8"
        >
          {/*Main row*/}
          <div className="flex h-20 items-center justify-between">
            {/* Logo */}
            <div className="flex shrink-0">
              <Link to="/" className="-m-1.5 p-1.5 text-lg font-semibold">
                Booking System
              </Link>
            </div>

            {/* Desktop / Tablet search */}
            <div className="hidden sm:block">
              <SearchBar />
            </div>

            {/*Right Actions */}
            <div className="flex items-center gap-2 sm-gap-3">
              {/*Become a host*/}
              <div className="hidden lg:flex">
                <button
                  type="button"
                  className="border border-none rounded-2xl px-3 py-2 font-medium hover:bg-gray-200 transition-all"
                >
                  Become a host
                </button>
              </div>
              <LanguageRegionMenu />
              <UserMenu />
            </div>
          </div>

          {/* Mobile search */}
          <div className="pb-4 sm:hidden">
            <SearchBar/>
          </div>
        </nav>
      </header>
    </>
  );
}
