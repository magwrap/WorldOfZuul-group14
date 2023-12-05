
objects = []
objectName = "mapObj"

for i in range(1, 21):
  objects.append(f"{objectName}{i}")
  if(i % 2 == 0):
    print(f"MapObject {objectName}{i} = new(3, {i}, MapObjectsEnum.VERTICALWALL);")

  else:
    print(f"MapObject {objectName}{i} = new(7, {i}, MapObjectsEnum.VERTICALWALL);")


print("RoomMap.mapEntities.PopulateMap(\n\tnew MapObject[]{")
for i in objects:
  print("\t\t" + i + ",")

print("});");