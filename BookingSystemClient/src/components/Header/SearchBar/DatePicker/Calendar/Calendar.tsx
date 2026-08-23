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

  return (
    <>
      <p className="mb-4 text-sm font-semibold">Select dates</p>

      <div>
        <div className="mb-4 text-center font-medium">August 2026</div>

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

        <div className="grid grid-cols-7 gap-1">
          {days.map((day, index) => {
            if (!day) {
              return <div key={index} />;
            }

            const date = new Date(currentYear, currentMonth, day);

            const isCheckIn = isSameDay(date, checkIn);
            const isCheckOut = isSameDay(date, checkOut);
            const isInRange = isDateInRange(date, checkIn, checkOut);

            return (
              <button
                type="button"
                onClick={() => handleDateSelect(date)}
                className={`
                        flex size-10 items-center justify-center rounded-full hover:bg-gray-100
                        ${isCheckIn || isCheckOut ? "bg-black text-white" : ""}
                        ${isInRange ? "bg-gray-200" : ""}
                        ${!isCheckIn && !isCheckOut && !isInRange ? "hover:bg-gray-100" : ""}
                      `}
              >
                {day}
              </button>
            );
          })}
        </div>
      </div>
    </>
  );
}
