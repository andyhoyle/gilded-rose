Feature: GuildedRoseTests

Given a list of items in stock, check that quality is updated correctly as days elapse

Scenario: Normal item quality degrades at the end of each day
	Given a list of items in stock:
	| Name   | SellIn | Quality |
	| Banana | 5      | 5       |
	When a single day has elapsed
	Then the quality of the item "Banana" should be 4

Scenario: Normal item quality is never negative
	Given a list of items in stock:
	| Name   | SellIn | Quality |
	| Banana | 3      | 1       |
    When 3 days have elapsed
    Then the quality of the item "Banana" should be 0

Scenario: Normal item quality degrades twice as fast after SellIn expired
	Given a list of items in stock:
	| Name   | SellIn | Quality |
	| Banana | 1      | 4       |
    When 2 days have elapsed
    Then the quality of the item "Banana" should be 1

Scenario: Normal item quality is lowest at zero after SellIn expired
	Given a list of items in stock:
	| Name   | SellIn | Quality |
	| Banana | 1      | 4       |
    When 3 days have elapsed
    Then the quality of the item "Banana" should be 0

Scenario: SellIn value is decremented by one for non-Sulfura item types
	Given a list of items in stock:
        | Name                                      | SellIn | Quality |
        | Banana                                    | 5      | 4       |
        | Aged Brie                                 | 5      | 4       |
        | Backstage passes to a TAFKAL80ETC concert | 5      | 4       |
    When a single day has elapsed
    Then the sell in of the item "Banana" should be 4
    Then the sell in of the item "Aged Brie" should be 4
    Then the sell in of the item "Backstage passes to a TAFKAL80ETC concert" should be 4

Scenario: "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	Given a list of items in stock:
	| Name                       | SellIn | Quality |
	| Sulfuras, Hand of Ragnaros | 5      | 80      |
    When a single day has elapsed
    Then the sell in of the item "Sulfuras, Hand of Ragnaros" should be 5
    And the quality of the item "Sulfuras, Hand of Ragnaros" should be 80

Scenario: "Aged Brie" actually increases in Quality the older it gets
    Given a list of items in stock:
	| Name      | SellIn | Quality |
	| Aged Brie | 5      | 4       |
    When a single day has elapsed
    Then the quality of the item "Aged Brie" should be 5

Scenario: "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
    Given a list of items in stock:
	| Name                                      | SellIn | Quality |
	| Backstage passes to a TAFKAL80ETC concert | 25     | 4       |
    When a single day has elapsed
    Then the quality of the item "Backstage passes to a TAFKAL80ETC concert" should be 5

Scenario: Aged Brie Max Quality 50
    Given a list of items in stock:
	| Name      | SellIn | Quality |
	| Aged Brie | 5      | 50      |
    When a single day has elapsed
    Then the quality of the item "Aged Brie" should be 50

Scenario: BSP Max Quality 50
    Given a list of items in stock:
	| Name                                      | SellIn | Quality |
	| Backstage passes to a TAFKAL80ETC concert | 5      | 50      |
    When a single day has elapsed
    Then the quality of the item "Backstage passes to a TAFKAL80ETC concert" should be 50

Scenario: BSP Quality increases by 2 when there are 10 days or less
    Given a list of items in stock:
	| Name                                      | SellIn | Quality |
	| Backstage passes to a TAFKAL80ETC concert | 9      | 4       |
    When a single day has elapsed
    Then the quality of the item "Backstage passes to a TAFKAL80ETC concert" should be 6

Scenario: BSP Quality increases by 3 when there are 5 days or less
    Given a list of items in stock:
	| Name                                      | SellIn | Quality |
	| Backstage passes to a TAFKAL80ETC concert | 4      | 4       |
    When a single day has elapsed
    Then the quality of the item "Backstage passes to a TAFKAL80ETC concert" should be 7

Scenario: Sulfura can only have quality of 80
    Given a list of items in stock:
	| Name                                      | SellIn | Quality |
	| Sulfuras, Hand of Ragnaros | 5      | 4       |
    When a single day has elapsed
    Then the quality of the item "Sulfuras, Hand of Ragnaros" should be 80

Scenario: Conjured items degrade in quality twice as fast as normal items
    Given a list of items in stock:
	| Name               | SellIn | Quality |
	| Conjured Mana Cake | 5      | 5       |
    When a single day has elapsed
    Then the quality of the item "Conjured Mana Cake" should be 3

Scenario: Conjured items cannot have quality less than zero
    Given a list of items in stock:
	| Name               | SellIn | Quality |
	| Conjured Mana Cake | 1      | 5       |
    When 2 days have elapsed
    Then the quality of the item "Conjured Mana Cake" should be 0