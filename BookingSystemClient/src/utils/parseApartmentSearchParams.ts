import type ApartmentSearchParams from "../types/ApartmentSearchParams";

function parseDate(value: string | null): Date | null {
  if (!value || !/^\d{4}-\d{2}-\d{2}$/.test(value)) {
    return null;
  }

  const [year, month, day] = value.split("-").map(Number);

  const date = new Date(year, month - 1, day);

  if (
    date.getFullYear() !== year ||
    date.getMonth() !== month - 1 ||
    date.getDate() !== day
  ) {
    return null;
  }

  return date;
}

function parseGuestCount(value: string | null): number {
  if (value === null) {
    return 0;
  }

  if (!/^\d+$/.test(value)) {
    return 0;
  }

  const count = Number(value);

  return Number.isSafeInteger(count) ? count : 0;
}

export default function parseApartmentSearchParams(
  params: URLSearchParams,
): ApartmentSearchParams {
  const destination = params.get("destination")?.trim() ?? "";

  const checkIn = parseDate(params.get("checkIn"));
  const checkOut = parseDate(params.get("checkOut"));

  const adults = parseGuestCount(params.get("adults"));
  const children = parseGuestCount(params.get("children"));
  const infants = parseGuestCount(params.get("infants"));
  const pets = parseGuestCount(params.get("pets"));

  return {
    destination,
    dates: {
      checkIn,
      checkOut,
    },
    guests: {
      adults,
      children,
      infants,
      pets,
    },
  };
}
