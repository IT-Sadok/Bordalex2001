import { useState } from "react";
import type CalendarProps from "./CalendarProps";
import { getDaysInMonth, getFirstDayOfMonth, isSameDay } from "./calendarUtils";

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

  const totalCells = firstDay + daysInMonth;

  const days = Array.from({ length: totalCells }, (_, index) => {
    if (index < firstDay) {
      return null;
    }

    return index - firstDay + 1;
  });

  while (days.length % 7 !== 0) {
    days.push(null);
  }

  const weeks = [];

  for (let i = 0; i < days.length; i += 7) {
    weeks.push(days.slice(i, i + 7));
  }

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

        <div>
          {weeks.map((week, weekIndex) => {
            const checkInIndex = week.findIndex((day) => {
              if (!day) return false;

              const date = new Date(currentYear, currentMonth, day);

              return isSameDay(date, checkIn);
            });

            const checkOutIndex = week.findIndex((day) => {
              if (!day) return false;

              const date = new Date(currentYear, currentMonth, day);

              return isSameDay(date, checkOut);
            });

            let rangeLeft = 0;
            let rangeWidth = 0;

            if (checkIn && checkOut) {
              const isStartWeek = checkInIndex !== -1;
              const isEndWeek = checkOutIndex !== -1;

              if (isStartWeek && isEndWeek) {
                rangeLeft = ((checkInIndex + 0.5) / 7) * 100;
                rangeWidth = ((checkOutIndex - checkInIndex) / 7) * 100;
              } else if (isStartWeek) {
                rangeLeft = ((checkInIndex + 0.5) / 7) * 100;
                rangeWidth = 100 - rangeLeft;
              } else if (isEndWeek) {
                rangeLeft = 0;
                rangeWidth = ((checkOutIndex + 0.5) / 7) * 100;
              } else {
                const isMiddleWeek = week.some((day) => {
                  if (!day) return false;

                  const date = new Date(currentYear, currentMonth, day);

                  return date > checkIn && date < checkOut;
                });

                if (isMiddleWeek) {
                  rangeLeft = 0;
                  rangeWidth = 100;
                }
              }
            }

            return (
              <div key={weekIndex} className="relative grid grid-cols-7">
                {rangeWidth > 0 && (
                  <div
                    className="absolute inset-y-0 z-0 bg-gray-100"
                    style={{
                      left: `${rangeLeft}%`,
                      width: `${rangeWidth}%`,
                    }}
                  />
                )}

                {week.map((day, dayIndex) => {
                  if (!day) {
                    return <div key={dayIndex} className="h-10" />;
                  }

                  const date = new Date(currentYear, currentMonth, day);

                  const isCheckIn = isSameDay(date, checkIn);
                  const isCheckOut = isSameDay(date, checkOut);

                  return (
                    <div
                      key={dayIndex}
                      className="relative flex h-10 items-center justify-center"
                    >
                      <button
                        type="button"
                        onClick={() => handleDateSelect(date)}
                        className={`
                        relative z-10 flex size-10 items-center justify-center rounded-full
                        ${
                          isCheckIn || isCheckOut
                            ? "bg-black text-white"
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
            );
          })}
        </div>
      </div>
    </>
  );
}
