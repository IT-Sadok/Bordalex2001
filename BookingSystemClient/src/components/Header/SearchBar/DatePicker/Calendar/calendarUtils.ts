export const getDaysInMonth = (year: number, month: number) => {
  return new Date(year, month + 1, 0).getDate();
};

export const getFirstDayOfMonth = (year: number, month: number) => {
  const day = new Date(year, month, 1).getDay();

  return day === 0 ? 6 : day - 1;
};

export const isSameDay = (date: Date, otherDate: Date | null) => {
  if (!otherDate) return false;

  return (
    date.getFullYear() === otherDate.getFullYear() &&
    date.getMonth() === otherDate.getMonth() &&
    date.getDate() === otherDate.getDate()
  );
};

export const isDateInRange = (
  date: Date,
  checkIn: Date | null,
  checkOut: Date | null,
) => {
  if (!checkIn || !checkOut) return false;

  return date > checkIn && date < checkOut;
};
