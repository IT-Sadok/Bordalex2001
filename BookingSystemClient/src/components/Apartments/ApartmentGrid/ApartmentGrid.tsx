import ApartmentCard from "../ApartmentCard/ApartmentCard";
import mockApartments from "../mockApartments";

export default function ApartmentGrid() {
  return (
    <div>
      {mockApartments.map((apartment) => (
        <ApartmentCard key={apartment.id} {...apartment} />
      ))}
    </div>
  );
}
