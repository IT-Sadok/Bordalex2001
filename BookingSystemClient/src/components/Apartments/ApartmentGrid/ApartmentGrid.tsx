import ApartmentCard from "../ApartmentCard/ApartmentCard";
import type ApartmentGridProps from "./ApartmentGridProps";

export default function ApartmentGrid({
  apartments,
  isLoading = false,
  error = null,
}: ApartmentGridProps) {
  if (isLoading) {
    return (
      <div className="py-12 text-center text-gray-500">
        Loading apartments...
      </div>
    );
  }

  if (error) {
    return (
      <div className="py-12 text-center">
        <p className="font-medium">Something went wrong</p>

        <p className="mt-1 text-sm text-gray-500">{error}</p>
      </div>
    );
  }

  if (apartments.length === 0) {
    return (
      <div className="py-12 text-center">
        <p className="font-medium">No apartments found</p>

        <p className="mt-1 text-sm text-gray-500">
          Try changing your search or filters.
        </p>
      </div>
    );
  }

  return (
    <div className="grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
      {apartments.map((apartment) => (
        <ApartmentCard key={apartment.id} {...apartment} />
      ))}
    </div>
  );
}
