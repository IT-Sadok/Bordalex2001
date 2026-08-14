import { Link } from "react-router";
import SearchBar from "./SearchBar";
import LanguageRegionMenu from "./LanguageRegionMenu";
import UserMenu from "./UserMenu";

export default function Header() {
  return (
    <>
      <header>
        <nav
          aria-label="Main Navigation"
          className="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8"
          role="navigation"
        >
          <div className="flex">
            <Link to="/" className="-m-1.5 p-1.5">
              Booking System
            </Link>
          </div>
          <SearchBar />
          <div className="flex lg:justify-end gap-2">
            <div className="flex">
              <button
                type="button"
                className="border border-none rounded-2xl px-3 hover:bg-gray-200 transition-all"
              >
                Become a host
              </button>
            </div>
            <LanguageRegionMenu/>
            <UserMenu/>
          </div>
        </nav>
      </header>
    </>
  );
}
