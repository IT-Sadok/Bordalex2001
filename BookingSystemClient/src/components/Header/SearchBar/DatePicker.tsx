import { useRef, useState } from "react";
import useClickOutside from "../../../hooks/useClickOutside";

const getDaysInMonth = (year: number, month: number) => {
  return new Date(year, month + 1, 0).getDate();
};

const getFirstDayOfMonth = (year: number, month: number) => {
  const day = new Date(year, month, 1).getDay();

  return day === 0 ? 6 : day - 1;
};

const currentYear = 2026;
const currentMonth = 7;

const daysInMonth = getDaysInMonth(currentYear, currentMonth);

const firstDay = getFirstDayOfMonth(currentYear, currentMonth);

const days = Array.from({ length: firstDay + daysInMonth }, (_, index) => {
  if (index < firstDay) {
    return null;
  }

  return index - firstDay + 1;
});

export default function DatePicker() {
  const [isOpen, setIsOpen] = useState(false);

  const datePickerRef = useRef<HTMLDivElement>(null);

  useClickOutside(datePickerRef, () => {
    setIsOpen(false);
  });

  return (
    <>
      <div ref={datePickerRef} className="relative flex-1">
        <button
          type="button"
          onClick={() => setIsOpen((prev) => !prev)}
          className="w-full rounded-2xl px-3 py-2 text-left hover:bg-gray-100"
        >
          <span className="block text-xs font-semibold">When</span>
          <span className="block truncate text-sm text-gray-500">Any week</span>
        </button>

        {isOpen && (
          <div className="absolute left-1/2 top-full z-50 mt-4 w-[350px] -translate-x-1/2 rounded-2xl border border-gray-200 bg-white p-4 shadow-lg">
            <p className="mb-4 text-sm font-semibold">Select dates</p>

            <div>
              <div className="mb-4 text-center font-medium">August 2026</div>

              <div className="grid grid-cols-7 mb-2">
                {["Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"].map(
                    (day) => (
                        <div
                        key={day}
                        className="text-center text-xs font-medium text-gray-500">
                            {day}
                        </div>
                    )
                )}
              </div>

              <div className="grid grid-cols-7 gap-1">
                {days.map((day, index) => (
                    <div key={index}>
                        {day && (
                            <button 
                            type="button"
                            className="flex size-10 items-center justify-center rounded-full hover:bg-gray-100"
                            >
                                {day}
                            </button>
                        )}
                    </div>
                ))}
              </div>
            </div>
          </div>
        )}
      </div>
    </>
  );
}
