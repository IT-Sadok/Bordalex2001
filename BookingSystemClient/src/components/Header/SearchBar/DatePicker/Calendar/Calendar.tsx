import { useState } from "react";
import type CalendarProps from "./CalendarProps";
import {
  getDaysInMonth,
  getFirstDayOfMonth,
  isDateInRange,
  isSameDay,
} from "./calendarUtils";

export default function Calendar({
  checkIn,
  checkOut,
  onCheckInChange,
  onCheckOutChange,
}: CalendarProps) {
  const [currentDate, setCurrentDate] = useState(new Date(2026, 7, 1));

  const currentYear = currentDate.getFullYear();
  const currentMonth = currentDate.getMonth();

  const daysInMonth = getDaysInMonth(currentYear, currentMonth);
  const firstDay = getFirstDayOfMonth(currentYear, currentMonth);

  const days = Array.from({ length: firstDay + daysInMonth }, (_, index) => {
    if (index < firstDay) {
      return null;
    }

    return index - firstDay + 1;
  });

  const handleDateSelect = (date: Date) => {
    if (!checkIn || checkOut) {
      onCheckInChange(date);
      onCheckOutChange(null);
      return;
    }

    if (date >= checkIn) {
      onCheckOutChange(date);
    } else {
      onCheckInChange(date);
      onCheckOutChange(null);
    }
  };

  const goToPreviousMonth = () => {
    setCurrentDate(
      (prev) => new Date(prev.getFullYear(), prev.getMonth() - 1, 1),
    );
  };

  const goToNextMonth = () => {
    setCurrentDate(
      (prev) => new Date(prev.getFullYear(), prev.getMonth() + 1, 1),
    );
  };

  const monthName = currentDate.toLocaleDateString("en-US", {
    month: "long",
    year: "numeric",
  });

  return (
    <>
      <p className="mb-4 text-sm font-semibold">Select dates</p>

      <div>
        <div className="mb-4 flex items-center justify-between">
          <button
            type="button"
            onClick={goToPreviousMonth}
            className="rounded-full p-2 hover:bg-gray-100"
            aria-label="Previous month"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              className="size-4"
            >
              <path
                fillRule="evenodd"
                d="M9.78 4.22a.75.75 0 0 1 0 1.06L7.06 8l2.72 2.72a.75.75 0 1 1-1.06 1.06L5.47 8.53a.75.75 0 0 1 0-1.06l3.25-3.25a.75.75 0 0 1 1.06 0Z"
                clipRule="evenodd"
              />
            </svg>
          </button>

          <div className="text-center font-medium">{monthName}</div>

          <button
            type="button"
            onClick={goToNextMonth}
            className="rounded-full p-2 hover:bg-gray-100"
            aria-label="Next month"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              className="size-4"
            >
              <path
                fillRule="evenodd"
                d="M6.22 4.22a.75.75 0 0 1 1.06 0l3.25 3.25a.75.75 0 0 1 0 1.06l-3.25 3.25a.75.75 0 0 1-1.06-1.06L8.94 8 6.22 5.28a.75.75 0 0 1 0-1.06Z"
                clipRule="evenodd"
              />
            </svg>
          </button>
        </div>

        <div className="grid grid-cols-7 mb-2">
          {["Mo", "Tu", "We", "Th", "Fr", "Sa", "Su"].map((day) => (
            <div
              key={day}
              className="text-center text-xs font-medium text-gray-500"
            >
              {day}
            </div>
          ))}
        </div>

        <div className="grid grid-cols-7">
          {days.map((day, index) => {
            if (!day) {
              return <div key={index} />;
            }

            const date = new Date(currentYear, currentMonth, day);

            const isCheckIn = isSameDay(date, checkIn);
            const isCheckOut = isSameDay(date, checkOut);
            const isInRange = isDateInRange(date, checkIn, checkOut);

            return (
              <div
                key={index}
                className="relative flex h-10 items-center justify-center"
              >
                {isInRange && (
                  <div className="absolute inset-y-0 left-0 right-0 z-0 bg-gray-100" />
                )}

                {isCheckIn && checkOut && (
                  <div className="absolute inset-y-0 left-1/2 right-0 z-0 bg-gray-100" />
                )}

                {isCheckOut && checkIn && (
                  <div className="absolute inset-y-0 left-0 right-1/2 z-0 bg-gray-100" />
                )}

                <button
                  type="button"
                  onClick={() => handleDateSelect(date)}
                  className={`
                        relative z-10 flex size-10 items-center justify-center rounded-full transition-colors
                        ${
                          isCheckIn || isCheckOut
                            ? "bg-black text-white"
                            : isInRange
                              ? "text-gray-900"
                              : "hover:bg-gray-100"
                        }
                      `}
                >
                  {day}
                </button>
              </div>
            );
          })}
        </div>
      </div>
    </>
  );
}
