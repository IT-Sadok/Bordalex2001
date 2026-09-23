export default interface DestinationPickerProps {
  destination: string;
  setDestination: (value: string) => void;
  error?: string;
}
