import type ApartmentCardProps from "./ApartmentCardProps";

export default function ApartmentCard({
  title,
  location,
  pricePerNight,
  rating,
  imageUrl,
}: ApartmentCardProps) {
  return (
    <article className="group cursor-pointer">
      {/*Apartment image */}
      <div className="relative aspect-square overflow-hidden rounded-xl">
        <img
          src={imageUrl}
          alt={title}
          className="h-full w-full object-cover transition-transform duration-300 group-hover:scale-105"
        />

        {/* Favorite button */}
        <button
          type="button"
          aria-label="Add to favorites"
          className="absolute right-3 top-3 flex size-8 items-center justify-center rounded-full bg-white/80 shadow-sm"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 16 16"
            fill="currentColor"
            className="size-4"
          >
            <path d="M2 6.342a3.375 3.375 0 0 1 6-2.088 3.375 3.375 0 0 1 5.997 2.26c-.063 2.134-1.618 3.76-2.955 4.784a14.437 14.437 0 0 1-2.676 1.61c-.02.01-.038.017-.05.022l-.014.006-.004.002h-.002a.75.75 0 0 1-.592.001h-.002l-.004-.003-.015-.006a5.528 5.528 0 0 1-.232-.107 14.395 14.395 0 0 1-2.535-1.557C3.564 10.22 1.999 8.558 1.999 6.38L2 6.342Z" />
          </svg>
        </button>
      </div>

      {/* Apartment information */}
      <div className="mt-3">
        <div className="flex items-start justify-between gap-2">
          <h2 className="truncate font-semibold">{title}</h2>

          <span className="shrink-0 text-sm">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 16 16"
              fill="currentColor"
              className="size-4"
            >
              <path
                fillRule="evenodd"
                d="M8 1.75a.75.75 0 0 1 .692.462l1.41 3.393 3.664.293a.75.75 0 0 1 .428 1.317l-2.791 2.39.853 3.575a.75.75 0 0 1-1.12.814L7.998 12.08l-3.135 1.915a.75.75 0 0 1-1.12-.814l.852-3.574-2.79-2.39a.75.75 0 0 1 .427-1.318l3.663-.293 1.41-3.393A.75.75 0 0 1 8 1.75Z"
                clipRule="evenodd"
              />
            </svg>
            {rating}
          </span>
        </div>

        <p className="truncate text-sm text-gray-500">{location}</p>

        <p className="mt-2 text-sm">
          <span className="font-semibold">${pricePerNight}</span> night
        </p>
      </div>
    </article>
  );
}
