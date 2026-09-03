import { useRef, useState } from "react";
import useClickOutside from "../../../../hooks/useClickOutside";
import Calendar from "./Calendar/Calendar";

export default function DatePicker() {
  const [isOpen, setIsOpen] = useState(false);
  const [checkIn, setCheckIn] = useState<Date | null>(null);
  const [checkOut, setCheckOut] = useState<Date | null>(null);

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
  }

  return (
    <>
      <div ref={datePickerRef} className="relative flex-1">
        <button
          type="button"
          onClick={() => setIsOpen((prev) => !prev)}
          className="w-full rounded-2xl px-3 py-2 text-left hover:bg-gray-100"
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

        {isOpen && (
          <div className="absolute left-1/2 top-full z-50 mt-4 w-87.5 -translate-x-1/2 rounded-2xl border border-gray-200 bg-white p-4 shadow-lg">
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
