import { useRef, useState } from "react";
import useClickOutside from "../../../../hooks/useClickOutside";
import Calendar from "./Calendar/Calendar";
import type DatePickerProps from "./DatePickerProps";

export default function DatePicker({
  checkIn,
  checkOut,
  setCheckIn,
  setCheckOut,
  error,
}: DatePickerProps) {
  const [isOpen, setIsOpen] = useState(false);

  const datePickerRef = useRef<HTMLDivElement>(null);

  useClickOutside(datePickerRef, () => {
    setIsOpen(false);
  });

  const formatDate = (date: Date) => {
    return date.toLocaleDateString("en-US", {
      month: "short",
      day: "numeric",
    });
  };

  const handleCheckOutChange = (date: Date | null) => {
    setCheckOut(date);

    if (date) {
      setIsOpen(false);
    }
  };

  return (
    <>
      <div ref={datePickerRef} className="relative h-full min-w-0 flex-1">
        <button
          type="button"
          onClick={() => setIsOpen((prev) => !prev)}
          className={`flex h-full w-full min-w-0 flex-col justify-center rounded-2xl px-2 md:px-3 text-left hover:bg-gray-100 ${error ? "ring-2 ring-red-500" : ""}`}
        >
          <span className="block text-xs font-semibold">When</span>
          <span className="block truncate text-sm text-gray-500">
            {!checkIn
              ? "Any week"
              : !checkOut
                ? formatDate(checkIn)
                : `${formatDate(checkIn)} - ${formatDate(checkOut)}`}
          </span>
        </button>

        {error && (
          <p className="absolute left-0 top-full z-20 mt-1 text-xs text-red-500">
            {error}
          </p>
        )}

        {isOpen && (
          <div className="absolute left-1/2 top-full z-50 mt-4 w-[min(21.875rem,calc(100vw-2rem))] -translate-x-1/2 rounded-2xl border border-gray-200 bg-white p-3 sm:p-4 shadow-lg">
            <Calendar
              checkIn={checkIn}
              checkOut={checkOut}
              onCheckInChange={setCheckIn}
              onCheckOutChange={handleCheckOutChange}
            />
          </div>
        )}
      </div>
    </>
  );
}
