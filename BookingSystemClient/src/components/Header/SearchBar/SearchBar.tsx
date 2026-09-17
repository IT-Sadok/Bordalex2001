import { useState } from "react";
import DatePicker from "./DatePicker/DatePicker";
import DestinationPicker from "./DestinationPicker/DestinationPicker";
import GuestPicker from "./GuestPicker/GuestPicker";
import type SearchErrors from "./SearchErrors";

export default function SearchBar() {
  const [destination, setDestination] = useState("");

  const [checkIn, setCheckIn] = useState<Date | null>(null);
  const [checkOut, setCheckOut] = useState<Date | null>(null);

  const [adults, setAdults] = useState(0);
  const [children, setChildren] = useState(0);
  const [infants, setInfants] = useState(0);
  const [pets, setPets] = useState(0);

  const [errors, setErrors] = useState<SearchErrors>({});

  const clearErrors = (field: keyof SearchErrors) => {
    setErrors((prev) => ({
      ...prev,
      [field]: undefined,
    }));
  };

  const handleDestinationChange = (value: string) => {
    setDestination(value);

    if (value) {
      clearErrors("destination");
    }
  }

  const handleCheckInChange = (date: Date | null) => {
    setCheckIn(date);

    if (date && checkOut && checkOut > date) {
      clearErrors("dates");
    }
  }

  const handleCheckOutChange = (date: Date | null) => {
    setCheckOut(date);

    if (checkIn && date && date > checkIn) {
      clearErrors("dates");
    }
  }

  const handleAdultsChange = (value: number) => {
    if (value >= 1) {
      clearErrors("guests");
    }
  }

  const handleSearch = () => {
    const errors: SearchErrors = {};

    if (!destination) {
      errors.destination = "Please select a destination";
    }

    if (!checkIn) {
      errors.dates = "Please select a check-in date";
    } else if (!checkOut) {
      errors.dates = "Please select a check-out date";
    } else if (checkOut <= checkIn) {
      errors.dates = "Check-out date must be after check-in date";
    }

    if (adults < 1) {
      errors.guests = "Please select at least one adult";
    }

    setErrors(errors);

    if (Object.keys(errors).length > 0) {
      return;
    }

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
      <form role="search" className="mx-auto w-full h-full max-w-xl">
        <div className="flex h-16 w-full items-stretch gap-1 px-2 py-2 border border-gray-300 rounded-3xl shadow-sm hover:shadow-lg transition-all">
          {/* Search options */}
          <div className="flex h-full min-w-0 flex-1 items-center">
            <DestinationPicker
              destination={destination}
              setDestination={handleDestinationChange}
              error={errors.destination}
            />
            <span className="border-l border-gray-300 h-6" />
            <DatePicker
              checkIn={checkIn}
              checkOut={checkOut}
              setCheckIn={handleCheckInChange}
              setCheckOut={handleCheckOutChange}
              error={errors.dates}
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
              onAdultsChange={handleAdultsChange}
              error={errors.guests}
            />
          </div>

          {/* Search button*/}
          <button
            type="button"
            onClick={handleSearch}
            className="flex h-8 w-8 shrink-0 items-center justify-center self-center bg-red-500 hover:bg-red-600 rounded-2xl transition-all active:scale-95 focus:outline-none focus:ring-2 focus:ring-red-300"
            aria-label="Search"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="white"
              className="size-4.5"
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
