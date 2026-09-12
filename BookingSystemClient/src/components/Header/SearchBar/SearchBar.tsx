import { useState } from "react";
import DatePicker from "./DatePicker/DatePicker";
import DestinationPicker from "./DestinationPicker/DestinationPicker";
import GuestPicker from "./GuestPicker/GuestPicker";

export default function SearchBar() {
  const [destination, setDestination] = useState("");

  const [checkIn, setCheckIn] = useState<Date | null>(null);
  const [checkOut, setCheckOut] = useState<Date | null>(null);

  const [adults, setAdults] = useState(0);
  const [children, setChildren] = useState(0);
  const [infants, setInfants] = useState(0);
  const [pets, setPets] = useState(0);

  const handleSearch = () => {
    const searchData = {
      destination,
      dates: { checkIn, checkOut },
      guests: {
        adults,
        children,
        infants,
        pets,
      },
    };
    console.log(searchData);
  };

  return (
    <>
      <form role="search" className="mx-auto w-full max-w-xl">
        <div className="flex w-full items-center gap-1 px-2 py-2 border border-gray-300 rounded-3xl hover:shadow-lg transition-all">
          {/* Search options */}
          <div className="flex min-w-0 flex-1 items-center">
            <DestinationPicker
              destination={destination}
              setDestination={setDestination}
            />
            <span className="border-l border-gray-300 h-6" />
            <DatePicker
              checkIn={checkIn}
              checkOut={checkOut}
              setCheckIn={setCheckIn}
              setCheckOut={setCheckOut}
            />
            <span className="border-l border-gray-300 h-6" />
            <GuestPicker
              adults={adults}
              children={children}
              infants={infants}
              pets={pets}
              setAdults={setAdults}
              setChildren={setChildren}
              setInfants={setInfants}
              setPets={setPets}
            />
          </div>

          {/* Search button*/}
          <button
            type="button"
            onClick={handleSearch}
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
