import type ApartmentCardProps from "../ApartmentCard/ApartmentCardProps";

export default interface ApartmentGridProps {
  apartments: ApartmentCardProps[];
  isLoading?: boolean;
  error?: string | null;
  onRetry?: () => void;
}
