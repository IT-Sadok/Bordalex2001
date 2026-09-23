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
          <div className="grid grid-cols-[1fr_auto] items-center gap-x-4 gap-y-3 py-4 sm:grid-cols-[auto_1fr_auto] sm:gap-x-6">
            {/* Logo */}
            <div className="shrink-0">
              <Link to="/" className="-m-1.5 p-1.5 text-lg font-semibold">
                Booking System
              </Link>
            </div>

            {/* Search — desktop/tablet position */}
            <div className="col-span-2 row-start-2 min-w-0 sm:col-span-1 sm:col-start-2 sm:row-start-1 sm:justify-self-center">
              <SearchBar />
            </div>

            {/*Right Actions */}
            <div className="col-start-2 row-start-1 flex items-center justify-self-end gap-2 sm:col-start-3 sm:gap-3">
              {/*Become a host*/}
              <div className="hidden lg:flex">
                <button
                  type="button"
                  className="rounded-2xl px-3 py-2 font-medium hover:bg-gray-200 transition-all"
                >
                  Become a host
                </button>
              </div>
              <LanguageRegionMenu />
              <UserMenu />
            </div>
          </div>
        </nav>
      </header>
    </>
  );
}
