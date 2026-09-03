import DatePicker from "./DatePicker/DatePicker";
import DestinationPicker from "./DestinationPicker";

export default function SearchBar() {
  return (
    <>
      <form role="search" className="mx-auto w-full max-w-xl">
        <div className="flex w-full items-center gap-1 px-2 py-2 border border-gray-300 rounded-3xl hover:shadow-lg transition-all">
          {/* Search options */}
          <div className="flex min-w-0 flex-1 items-center">
            <DestinationPicker />
            <span className="border-l border-gray-300 h-6" />
            <DatePicker />
            <span className="border-l border-gray-300 h-6" />
            <button
              type="button"
              className="min-w-0 flex-1 truncate px-2 py-2 text-center text-sm font-medium sm:px-3 sm:text-base hover:bg-gray-100 rounded-2xl"
            >
              Add guests
            </button>
          </div>

          {/* Search button*/}
          <button
            type="submit"
            className="flex shrink-0 items-center justify-center bg-red-500 hover:bg-red-600 p-2 rounded-2xl transition-all"
            aria-label="Search"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="white"
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
      </form>
    </>
  );
}
