import type { Dispatch, SetStateAction } from "react";

export default interface GuestPickerProps {
  adults: number;
  children: number;
  infants: number;
  pets: number;

  setAdults: Dispatch<SetStateAction<number>>;
  setChildren: Dispatch<SetStateAction<number>>;
  setInfants: Dispatch<SetStateAction<number>>;
  setPets: Dispatch<SetStateAction<number>>;

  onAdultsChange?: (adults: number) => void;

  error?: string;
}
