import type { Dispatch, SetStateAction } from "react";

export default interface DestinationPickerProps {
  destination: string;
  setDestination: Dispatch<SetStateAction<string>>;
  error?: string;
}
