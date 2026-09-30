import ApartmentCard from "../ApartmentCard/ApartmentCard";
import type ApartmentGridProps from "./ApartmentGridProps";

export default function ApartmentGrid({ apartments }: ApartmentGridProps) {
  return (
    <div className="grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
      {apartments.map((apartment) => (
        <ApartmentCard key={apartment.id} {...apartment} />
      ))}
    </div>
  );
}
