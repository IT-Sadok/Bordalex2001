export default interface ApartmentCardProps {
  id: string;
  title: string;
  location: string;
  pricePerNight: number;
  rating?: number;
  imageUrl?: string;
}
