import type { Dispatch, SetStateAction } from "react";

export default interface DatePickerProps {
  checkIn: Date | null;
  checkOut: Date | null;
  setCheckIn: Dispatch<SetStateAction<Date | null>>;
  setCheckOut: Dispatch<SetStateAction<Date | null>>;
}
