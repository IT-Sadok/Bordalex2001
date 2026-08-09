export default function SearchBar() {
    return (
    <>
      <form role="search" className="max-w-md mx-auto w-full h-full px-4">
        <div className="relative flex items-center space-between gap-x-4 px-2 py-2 border border-gray-300 rounded-3xl hover:shadow-lg transition-all">
          <div className="flex items-center basis-11/12">
            <div className="text-base text-center font-medium w-full">
              Anywhere
            </div>
            <span className="border border-gray-300 h-6 mx-2"></span>
            <div className="text-base text-center font-medium w-full">
              Any week
            </div>
            <span className="border border-gray-300 h-6 mx-2"></span>
            <div className="text-base text-center font-medium w-full">
              Add guests
            </div>
          </div>
          <div className="flex basis-1/12">
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
};